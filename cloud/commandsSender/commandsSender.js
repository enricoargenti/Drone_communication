/*
    Client that will send commands to drones
*/

const { Console } = require('console');
const mqtt = require('mqtt')
const prompt = require('prompt-sync')();
const client  = mqtt.connect('mqtt://127.0.0.1')

// asks which is the specific drone to check
const droneId = prompt('Drone Id to which send commands:');

var topic = `iot2022test/commands/${droneId}`;

// commands insertion -------------------------------------------------------------------------------------------
commands = ["apri corsa", "chiudi corsa", "accendi", "spegni", "rientra alla base", "accendi led di posizione"];

while(true)
{
  console.log("topic: " + topic);
  var inputCommand = prompt('Command to send: ');
  client.publish(topic, inputCommand);
  //client.end()
  console.log("inviato\n")
}