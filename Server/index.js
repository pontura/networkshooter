var io = require('socket.io')(process.env.PORT || 52300);

io.on('connection',function (socket)
    {
        console.log("Connection made");
        
        socket.on('disconnect', function()
        {
            console.log("disconnect");
        })
    }
)