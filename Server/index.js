var io = require('socket.io')(process.env.PORT || 52300);
var Player = require('./Classes/Player.js');

var players = [];
io.on('connection',function (socket)
    {
        console.log("Connection made");

        var player = new Player();
        var thisPlayerid = player.id;

        players[thisPlayerid] = player;
        
        socket.on('disconnect', function()
        {
            console.log("disconnect");
        })
    }
)