var io = require('socket.io')(process.env.PORT || 52300);
var Player = require('./Classes/Player.js');

var players = [];
var sockets = [];

io.on('connection',function (socket)
    {
        console.log("Connection made");

        var player = new Player();
        var thisPlayerid = player.id;

        players[thisPlayerid] = player;
        sockets[thisPlayerid] = socket;

        socket.emit("register", {id:thisPlayerid}); //tell only the client:
        socket.emit("spawn", player); //me dice que yo spawnié
        socket.broadcast.emit("spawn", player);// le dice a los otros que entré

        //te avisa a vos que entró otro:
        for(var playerID in players)
        {
            if(playerID != thisPlayerid)
            {
                socket.emit("spawn", players[thisPlayerid]);
            }
        }
        socket.on('updatePosition', function(data)
        {
          //  console.log("x: " + data.position.x + " y: " + data.position.y);
            player.position.x = data.position.x;
            player.position.y = data.position.y;
           
            socket.broadcast.emit("updatePosition", player);
        })

        socket.on('disconnect', function()
        {
            console.log("disconnect");
        })
    }
)