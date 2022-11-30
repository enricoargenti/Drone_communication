Primo progetto corso IoT (gruppo Argenti Enrico ed Eupani Alessio)

-------------------------------------------------------------------------------------------------------------------
    Stato dell'arte:   

    Vengono generati quattro diverse misurazioni riferite ad un drone n. 
    Il drone può esporre i dati attraverso il broker Mosquitto (che viene fatto girare in locale su Docker). 
    
    Dal lato della ricezione dei dati dei sensori abbiamo due diverse modalità:
        - un client (subscriberDataStorage.js) riceve tutti i dati generati da tutti i droni
        attaverso l'iscrizione al topic iot2022test/# e li salva sul database Postgres 
        sfruttando le chiamate del provider (provider.js)

        - un client dedicato (subscriberSpeed.js) per la gestione dei dati della velocità di tutti i droni
        che nelle intenzioni verrà clonato per la gestione dei dati delle altre misurazioni

    Dal lato dell'invio dei comandi si sta sviluppando invece un commandsSender (commandsSender.js) che invia
    comandi inseriti da shell dall'utente. 
    Dal lato client del drone si dovrà in futuro implementare la gestione dei messaggi ricevuti 
    successivamente all'iscrizione al topic iot2022test/commands/#

    Da fare l'uso del flag_retain