/*
Imagine that one of your drones disappeared one hour ago and you noticed it only now.
A good way to know its last available position is to use flag retain. 
*/

/*
    Client subscribed to receive the last position value
    of a specific drone
*/
const mqtt = require('mqtt')
const prompt = require('prompt-sync')();
const client  = mqtt.connect('mqtt://127.0.0.1')

// asks which is the specific drone to check
const droneId = prompt('Insert your missing drone id: ');

var topic = 'iot2022test/' + droneId + '/Position';

client.on('connect', function () {
    console.log("Connesso");
    client.subscribe([topic], () => {
      console.log(`Subscribe to topic '${topic}'`)
    });

})

client.on('message', async function (topic, message) {
  //console.log("TOPIC: " + topic + "\n");

  var newPosition = {};

  var arr_from_json = JSON.parse(message.toString());

  newPosition.type  = arr_from_json.Type;
  newPosition.time = arr_from_json.Time;
  newPosition.latitude = arr_from_json.Latitude;
  newPosition.longitude = arr_from_json.Longitude;

  console.log("Last position available for your drone " + droneId + " is in date " + newPosition.time + " : ");
  console.log("Longitude: " + newPosition.longitude + " Latitude: " + newPosition.latitude);

  
})

