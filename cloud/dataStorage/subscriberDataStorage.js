const mqtt = require('mqtt')
var provider = require('./provider');

const client  = mqtt.connect('mqtt://127.0.0.1')

const topic = 'iot2022test/#';

client.on('connect', function () {
    console.log("Connesso");
    client.subscribe([topic], () => {
      console.log(`Subscribe to topic '${topic}'`)
    });

})

client.on('message', async function (topic, message) {
    console.log('TOPIC: ' + topic + "\nMESSAGE: " + message.toString());

    // insertion into the db calling a provider
    var newStatus = {};

    var arr_from_json = JSON.parse(message.toString());
    var topic_array = topic.split("/");

    newStatus.droneID = topic_array[1]; //gets the id at the second position on topic
    newStatus.type  = arr_from_json.Type;
    newStatus.time = arr_from_json.Time;

    //saves JSON packets that contain useful information about the drone status
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


    // prints to check values
    console.log("DroneID: " + newStatus.droneID);
    console.log("Type: " + newStatus.type);
    console.log("Time: " + newStatus.time);
    console.log("dataJSON: " + newStatus.dataJSON);

    // it adds the new status
    var last = await provider.addStatus(newStatus);
    if(last == undefined) {
        console.log("ERRORE di trasmissione al database: internalServerError");
        //throw internalServerError();
    }
    //res.code(201);
  
})

