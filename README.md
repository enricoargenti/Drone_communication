# IoT Project (Argenti Enrico, Eupani Alessio)

## AMQP

### Comunicazione dati su Redis ed estrazione dallo stesso
Completato sfruttando una lista Redis che funge da accumulatore di dati. 

### Comunicazione dati da producer a RabbitMQ : 
* implementata connessione con CloudAmqp e creato un nuovo *exchange* `myExchange` di tipo *topic*
* create due queues con rispettivi bindings: i topic corrispondenti sono:
* `drones.measurements` per la raccolta di tutti i dati rilevati dal drone
* `drones.measurements.positions` per la raccolta di tutti i dati riguardanti la posizione

### Comunicazione dati da RabbitMQ ai consumers: 
Sono state implementate due diverse soluzioni per la gestione dei dati presenti sul *broker AMQP*:
* `dataStorage`, dove sfruttando un consumer iscritto al *topic* `drones.measurements`
i pacchetti vengono scomposti ed inseriti nel database Postgres tramite il provider *provider.js*
* `positionControl`, dove prelevando dal *topic* `drones.measurements.positions`
i pacchetti riguardanti la posizione potranno essere elaborati a piacimento. 

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