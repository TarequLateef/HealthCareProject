//Create Connection 
var connect = new signalR.HubConnectionBuilder().withUrl("/confirmBook").build();

//Connect the method server invoked in the client
connect.on("AddBook", (value) => {
    /*console.log("New Patient " & value.PatientBaseTBL.PatientName);*/
    console.log("Views: ", value);
});

//Invoke the Hub Method from the client 
function AddNewBook() {
    connect.send("Confirmation");
}

//when the connection done 
function doneConnect() {
    console.log("Done");
    AddNewBook();
}
//when the connection make a proplem
function errConnect() {
    console.log("Error: " );
}
//Start connection
connect.start().then(doneConnect, errConnect);

