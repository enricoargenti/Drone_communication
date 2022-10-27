var {Client} = require('pg');

//Per le query usiamo le Promises al posto delle callback:
async function Run(){
    var client = new Client({
        user: 'postgres',
        host: 'localhost',
        database: 'tickets', 
        password: 'password',
        port: 5432,
    });
    
    client.connect();

    var res = await client.query("SELECT * FROM tickets;");
    console.log(res.rows);

    client.end();
    
}

Run();

module.exports.addUser = async function(newUser) {
    var client = getDbClient();
    var insert = await client.query(
        "INSERT INTO users (firstname, lastname, username) VALUES ($1, $2, $3) RETURNING id;",
    [newUser.firstname, newUser.lastname, newUser.username]);
    var id = insert.rows[0].id;
    var last = await client.query("SELECT * FROM users WHERE id = $1", [id]);
    client.end();
    return last.rows[0];
}