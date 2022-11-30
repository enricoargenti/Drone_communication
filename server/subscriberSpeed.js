/*
    Client subscribed to receive every speed value
    of a specific drone
*/
const mqtt = require('mqtt')
const prompt = require('prompt-sync')();
const client  = mqtt.connect('mqtt://127.0.0.1')

// asks which is the specific drone to check
const droneId = prompt('Drone Id to check:');

var topic = 'iot2022test/+/Speed';

client.on('connect', function () {
    console.log("Connesso");
    client.subscribe([topic], () => {
      console.log(`Subscribe to topic '${topic}'`)
    });

})

client.on('message', async function (topic, message) {
  console.log('TOPIC: ' + topic + "\nMESSAGE: " + message.toString());

  var newSpeed = {};

  var arr_from_json = JSON.parse(message.toString());

  // newSpeed.droneID = topic.substring(); // Capire come estrarlo dalle wildcards
  newSpeed.type  = arr_from_json.Type;
  newSpeed.time = arr_from_json.Time;
  newSpeed.value = arr_from_json.Value;

  //console.log("DroneID: " + newSpeed.droneID);
  console.log("Type: " + newSpeed.type);
  console.log("Time: " + newSpeed.time);
  console.log("dataJSON: " + newSpeed.value + "\n");
  
})

