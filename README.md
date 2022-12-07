# IoT Project (Argenti Enrico, Eupani Alessio)


## **Stato dell'arte:**  


### AMQP WORKING...
Comunicazione dati su Redis ed estrazione dallo stesso: COMPLETATA
Comunicazione dati da producer a RabbitMQ : implementata connessione con CloudAmqp, ma c'è 
una mancanza sul binding che non permette di mandare i dati con successo
Comunicazione dati da RabbitMQ ai consumers: da fare. 

### Generazione casuale ed invio misurazioni drone:

Vengono generate quattro diverse misurazioni riferite ad un drone n:
* Speed
* Battery Level
* Height
* Position


Il drone espone i dati attraverso il *topic* `iot2022test/drone_id/nome_sensore`, 
dove nome_sensore corrisponde al tipo di dato (as es. Speed) che viene inviato. 

Il *payload* corrisponde ad un pacchetto JSON contenente:
* Type (tipo di dato rilevato)
* Value (valore rilevato)
* Time (data e ora di rilevazione del dato)

