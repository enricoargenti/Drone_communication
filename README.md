# IoT Project (Argenti Enrico, Eupani Alessio)


## **Stato dell'arte:**  


### AMQP WORKING...
* Comunicazione dati su Redis ed estrazione dallo stesso: COMPLETATA
* Comunicazione dati da producer a RabbitMQ : implementata connessione con CloudAmqp e inserimento
  su una queue tramite:
	exchange di tipo *topic*
	binding con binding key *drones.measurements*

* Comunicazione dati da RabbitMQ ai consumers: implementata la connessione che porta i pacchetti
  dal broker CloudAmqp all'elaborazione in *dataStorage*, dove vengono scomposti ed inseriti 
  nel database Postgres tramite il provider *provider.js*

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

