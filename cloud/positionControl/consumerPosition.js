const amqplib = require('amqplib');

const url = "amqps://erzcddln:Noq08Uww2NHycbvZHuarnYOZbYdigSQG@whale.rmq.cloudamqp.com/erzcddln";
const main_queue = "drones.measurements.positions";


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


      ch1.ack(msg);
    } 
    else 
    {
      console.log('Consumer cancelled by server');
    }
  });

})();