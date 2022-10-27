var restify = require('restify');

var server = restify.createServer();
server.use(restify.plugins.bodyParser());

server.get('/drones', function(req, res, next) {
    res.send('List of drones: ' + req.params);
    return next();
});

server.get('/drones/:id', function(req, res, next) {
    res.send('Current values for drone ' + req.params['id'] + ': ' + req.params);
    console.log("URL : " + req.params.toString());
    return next();
});

// Ricezione singolo dato
server.put('/drones/:id/status/speed', function(req, res, next) {

});

// Ricezione pacchetto dati (posizione, velocità, ...)
server.put('/drones/:id/status', function(req, res, next) {

});

//Qui ricevo i dati e li mando al db
server.post('/drones/:id', function(req, res, next) {
    res.send('Data received from drone [TODO]' + req.body);

    console.log("Drone " + req.params['id']);
    console.log(req.body);
    console.log("");

    return next();
});

server.listen(8011, function() {
    console.log('%s listening at %s', server.name, server.url);
});