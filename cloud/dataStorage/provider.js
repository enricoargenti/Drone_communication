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

// Metodo che restituisce il numero di droni memorizzati nel database
module.exports.getNumberOfDrones = async function() {
    var client = getDbClient();
    var result = await client.query("SELECT COUNT(*) AS number_of_drones FROM drones;");
    client.end();
    return result.rows[0];
}

// Metodo che riceve come parametro l'id di un drone
// e restituisce tutti gli status salvati nel database riferiti ad un determinato drone
module.exports.getDroneStatuses = async function(id) {
    var client = getDbClient();
    var result = await client.query(
        "SELECT * FROM statuses WHERE drone_id = $1;" [id]);
    client.end();
    return result.rows;
}


// Metodo che inserisce nel database un nuovo status comprensivo di:
    // - tipo di misurazione (es. velocità)
    // - data e ora in cui il valore è stato misurato
    // - il dato/i dati misurati in formato JSON
    // - l'identificativo del drone da cui provengono i dati
// Il metodo accetta in input un oggetto newStatus che raccoglie il pacchetto dati arrivato dal drone
// e restituisce l'ultima riga aggiunta al database
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