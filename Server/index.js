var io = require('socket.io')(process.env.PORT || 52300);
var Player = require('./Classes/Player.js');

var players = [];
var sockets = [];
var id = 0;

io.on('connection',function (socket)
    {
        console.log("Connection made");

        var player = new Player();
        var thisPlayerid = player.id;
        
        players[thisPlayerid] = player;
        sockets[thisPlayerid] = socket;

        id++;

        player.num = id;

        socket.emit("register", {id:thisPlayerid}); //tell only the client:
        socket.emit("spawn", player); //me dice que yo spawnié
        socket.broadcast.emit("spawn", player);// le dice a los otros que entré

        console.log("player.num " + player.num + "   thisPlayerid : " + thisPlayerid);

        //te avisa a vos que habia otros:
        for(var otherPlayeID in players)
        {
            if(otherPlayeID != thisPlayerid)
            {
                socket.emit("spawn", players[otherPlayeID]);
            }
        }
        socket.on('updatePosition', function(data)
        {
           // console.log("x: " + data.position.x + " y: " + data.position.y);
            player.position.x = data.position.x;
            player.position.y = data.position.y;
           
            socket.broadcast.emit("updatePosition", player);
        })

        socket.on('shoot', function(data)
        {
           // console.log("shoot x: " + data.position.x + " y: " + data.position.y);
            player.position.x = data.position.x;
            player.position.y = data.position.y;
           
            socket.broadcast.emit("shoot", player);
        })

        socket.on('disconnect', function()
        {
            console.log("disconnect " + thisPlayerid);
            delete players[thisPlayerid];
            delete sockets[thisPlayerid];
            socket.broadcast.emit("disconnected", player);
        })
    }
)