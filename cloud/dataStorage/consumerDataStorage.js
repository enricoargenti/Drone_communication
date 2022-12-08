const provider = require('./provider');
const amqplib = require('amqplib');

const url = "amqps://erzcddln:Noq08Uww2NHycbvZHuarnYOZbYdigSQG@whale.rmq.cloudamqp.com/erzcddln";
const main_queue = "drones.measurements";


(async () => {
  const queue = main_queue;
  const conn = await amqplib.connect(url);

  const ch1 = await conn.createChannel();
  await ch1.assertQueue(queue);

  // Listener
  ch1.consume(queue, async (msg) => {
    if (msg !== null) 
    {
      //console.log('Received:', msg.content.toString());

      var message = JSON.parse(msg.content.toString());
      //console.log("Parsed JSON: " + message);
      var droneID = JSON.parse(message.Drone_ID);
      var data = JSON.parse(message.Data);

      // insertion into the db calling a provider
      var newStatus = {};

      newStatus.droneID = droneID; //gets the id at the second position on topic
      newStatus.type  = data.Type;
      newStatus.time = data.Time;

      //saves JSON packets that contain useful information about the drone status
      var newJSON = {};
      if(newStatus.type == "Position")
      {
          newJSON.Latitude = data.Latitude;
          newJSON.Longitude = data.Longitude;
      }
      else
      {
          newJSON.Value = data.Value;
      }
      newStatus.dataJSON = JSON.stringify(newJSON);


      // prints to check values
      console.log("DroneID: " + newStatus.droneID);
      console.log("Type: " + newStatus.type);
      console.log("Time: " + newStatus.time);
      console.log("dataJSON: " + newStatus.dataJSON);
      console.log();

      // it adds the new status
      var last = await provider.addStatus(newStatus);
      if(last == undefined) {
          console.log("ERRORE di trasmissione al database: internalServerError");
          //throw internalServerError();
      }
      //res.code(201);


      ch1.ack(msg);
    } 
    else 
    {
      console.log('Consumer cancelled by server');
    }
  });

})();





/*
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

*/