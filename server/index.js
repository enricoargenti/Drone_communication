
var restify = require('restify');
var provider = require('./provider');

var server = restify.createServer();
server.use(restify.plugins.bodyParser());


server.get('/drones', async function(req, res, next) {
    var drones = await provider.getNumberOfDrones();
    res.send(drones);
    return next();
});


server.get('/drones/:id', async function(req, res, next) {
    var id = req.params['id'];
    var drone = await provider.getDrone(id);
    //var droneStatuses = await provider.getDroneStatuses(id);
    //drone.statuses = droneStatuses;
    if(drone == undefined) {
        console.log("Eccezione da gestire: throw fastify.httpErrors.notFound()");
    }
    //res.send('Current values for drone ' + req.params['id'] + ': ' + droneStatuses);
    res.send("ciao");
    return next();
});


// Ricezione singolo dato
server.put('/drones/:id/status/speed', function(req, res, next) {

});

// Ricezione pacchetto dati (posizione, velocità, ...)
server.put('/drones/:id/status', function(req, res, next) {

});

//Qui ricevo i dati e li mando al db
server.post('/drones/:id', async function(req, res, next) {
    //var newStatus = {droneID, type, latitude, longitude, time, value};
    var newStatus = {};

    var arr_from_json = JSON.parse(req.body);

    newStatus.droneID = req.params['id'];
    newStatus.type  = arr_from_json.Type;
    newStatus.time = arr_from_json.Time;

    //GLI PASSO DATI IMPACCHETTATI IN JSON E LI SALVO IN JSON NEL DATABASE
    var newJSON = {};
    if(newStatus.type == "Position")
    {
        newJSON.Latitude = arr_from_json.Latitude;
        newJSON.Longitude = arr_from_json.Longitude;
    }
    else
    {
        newJSON.Value = arr_from_json.Value;
    }
    newStatus.dataJSON = JSON.stringify(newJSON);



    console.log("DroneID: " + newStatus.droneID);
    console.log("Type: " + newStatus.type);
    console.log("Time: " + newStatus.time);
    console.log("dataJSON: " + newStatus.dataJSON);

    
    var last = await provider.addStatus(newStatus);
    if(last == undefined) {
        console.log("ERRORE di trasmissione al database: internalServerError (da cercare sulla documentazione di Restify");
        //throw restify.httpErrors.internalServerError();
    }
    //res.code(201);
    res.send('Data received from drone ' + req.params['id'] + ' : ' + req.body);

    return next();
});

server.listen(8011, function() {
    console.log('%s listening at %s', server.name, server.url);
});

