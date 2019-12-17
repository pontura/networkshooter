var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(4567);

io.on('connection', function(socket){
	console.log("connection made");
	socket.on('beep', function(){
		socket.emit('boop');
	});
})
console.log("server started");
