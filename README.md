Primo progetto corso IoT (gruppo Argenti Enrico ed Eupani Alessio)

-------------------------------------------------------------------------------------------------------------------
                                        Stato dell'arte:   
-------------------------------------------------------------------------------------------------------------------


    ---------------------------------------------------
    | Generazione casuale ed invio misurazioni drone:

        Vengono generate quattro diverse misurazioni riferite ad un drone n:
            - Speed
            - Battery Level
            - Height
            - Position


        Il drone espone i dati attraverso il topic iot2022test/drone_id/nome_sensore, 
        dove nome_sensore corrisponde al tipo di dato (as es. Speed) che viene inviato. 

        Il payload corrisponde ad un pacchetto JSON contenente:
            - Type (tipo di dato rilevato)
            - Value (valore rilevato)
            - Time (data e ora di rilevazione del dato)


    ---------------------------------------------------
    | Broker MQTT:

        A garanzia della comunicazione tramite protocollo MQTT si sfrutta un broker Mosquitto 
        (che viene fatto girare in locale attraverso Docker). 


    ---------------------------------------------------
    | Ricezione ed elaborazione/storage dati drone:
    
        Dal lato della ricezione dei dati dei sensori abbiamo tre diverse modalità di gestione:

            - dataStorage: un client (subscriberDataStorage.js) riceve tutti i dati generati da tutti i droni
            attaverso l'iscrizione al topic iot2022test/# e li salva sul database Postgres 
            sfruttando le chiamate del provider (provider.js);

            - positionController: un client dedicato (positionSubscriber.js) che sfruttando l'opzione flag retain
            restituisce l'ultima posizione disponibile di un determinato drone (la situazione immaginata è quella
            in cui il drone non dovesse più rispondere e si volesse capire in che punto qualcosa è andato storto);

            - speedController: un client dedicato (subscriberSpeed.js) per la gestione dei dati della velocità 
            di tutti i droni (nelle intenzioni ogni tipo di misurazione proveniente dal drone 
            verrà gestita con un subscriber apposito). 


    ---------------------------------------------------
    | Invio di comandi al drone da remoto: 

        commandsSender : per la generazione e l'invio di comandi verso il drone si è sviluppato 
        invece un commandsSender (commandsSender.js) che pubblica i comandi inseriti da shell 
        dall'utente sul topic iot2022test/commands/drone_id. 
        Il payload per ora corrisponde ad una semplice stringa contenente un comando. 
        Successivamente si dovrà aggiungere un id per il comando (per garantire che
        si possa gestire la risposta asincrona di avvenuta esecuzione dello stesso). 

    ---------------------------------------------------
    | Ricezione dei comandi da parte del drone:

        Dal lato client del drone l'iscrizione viene fatta al topic iot2022test/commands/#
        (per ora come test si ricevono tutti i comandi inviati a qualsiasi drone). 

        Si è riscotrato un problema nella gestione dei messaggi in arrivo da questo topic:
        nei prossimi giorni si cercherà di implementare un metodo per mandare al cloud
        un messaggio di avvenuta ricezione del comando sul topic iot2022test/confirmCommands/#
        con payload del tipo "Ho eseguito il comando {comando} a seguito della richiesta {id_comando}". 
        Si dovrà in futuro implementare la gestione dei messaggi ricevuti 
        successivamente all'iscrizione al topic iot2022test/commands/#


    ---------------------------------------------------
    | TODOS: 

        Da fare: 
            - l'interfaccia grafica di visualizzazione dello stato del drone
            - sostituzione del db Postgres con InfluxDb