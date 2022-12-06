var {Client} = require('pg');

function getDbClient() {
    var client = new Client({
        user: 'enrico',
        host: 'localhost',
        database: 'dronedb', 
        password: 'enrico',
        port: 5432,
    });
    
    client.connect();
    return client;
    
}

// returns a single drone description (accepting a drone id as parameter)
module.exports.getDrone = async function(id) {
    var client = getDbClient();
    var result = await client.query("SELECT * FROM drones WHERE drone_id = $1 ;" [id]);
    client.end();
    return result.rows[0];
}

// returns the number of drones stored into the database
module.exports.getNumberOfDrones = async function() {
    var client = getDbClient();
    var result = await client.query("SELECT COUNT(*) AS number_of_drones FROM drones;");
    client.end();
    return result.rows[0];
}

// returns every status stored into the db that is referred to a certain drone
// (accepting a drone id as parameter)
module.exports.getDroneStatuses = async function(id) {
    var client = getDbClient();
    var result = await client.query(
        "SELECT * FROM statuses WHERE drone_id = $1;" [id]);
    client.end();
    return result.rows;
}

/* 
inserts into the database a new status that contains:
    - measurement type (i.e. "Speed")
    - date and hour of value measurement
    - measured data in JSON format
    - drone identifier from which data are taken
This method accepts as input (parameter) this new status and returns the last row added to the db. 
*/
module.exports.addStatus = async function(newStatus) {
    var client = getDbClient();
    var insert = await client.query(
        "INSERT INTO statuses (status_type, measurement_time, dataJSON, drone_id) VALUES ($1, $2, $3, $4)  RETURNING id;",
    [newStatus.type, newStatus.time, newStatus.dataJSON, newStatus.droneID]);
    var id = insert.rows[0].id;
    var last = await client.query("SELECT * FROM statuses WHERE id = $1", [id]);
    client.end();
    return last.rows[0];
}