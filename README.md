# IoT Project (Argenti Enrico, Eupani Alessio)

## HTTP

### Generazione casuale ed invio misurazioni drone:

Vengono generate quattro diverse misurazioni riferite ad un drone n:
* Speed
* Battery Level
* Height
* Position


Il drone espone i dati attraverso l'URI `http://localhost:8011/drones/{drone_number}/`.

Il messaggio inviato corrisponde ad un pacchetto JSON contenente:
* Type (tipo di dato rilevato)
* Value (valore rilevato)
* Time (data e ora di rilevazione del dato)
    
### Ricezione ed elaborazione/storage dati drone:
    
Dal lato della ricezione dei dati dei sensori abbiamo per il momento una sola modalità di gestione:

* dataStorage: un client `index.js` che riceve tutti i dati generati da tutti i droni
attaverso l'URI `/drones/:id` e li salva sul database *Postgres* 
sfruttando le chiamate del *provider* `provider.js`;
