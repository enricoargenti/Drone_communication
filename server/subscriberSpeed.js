const mqtt = require('mqtt')

const client  = mqtt.connect('mqtt://127.0.0.1')

const topic = 'iot2022test/+/Speed';

client.on('connect', function () {
    console.log("Connesso");
    client.subscribe([topic], () => {
      console.log(`Subscribe to topic '${topic}'`)
    });

})

client.on('message', async function (topic, message) {
  console.log('TOPIC: ' + topic + "\nMESSAGE: " + message.toString());

  var newSpeed = JSON.parse(message.toString());

  console.log("New speed: " + newSpeed)
  // console.log("DroneID: " + newStatus.droneID);
  
})

