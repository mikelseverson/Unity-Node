var io = require('socket.io')(process.env.PORT || 3000);

console.log('server started');

var playerCount = 0;

io.on('connection', (socket) => {
  console.log('client connected, broadcasting spawn');

  socket.broadcast.emit('spawn');
  playerCount++;

  for(i=0; i < playerCount; i++) {
    socket.emit('spawn');
    console.log('sending spawn to new player')
  };

  socket.on('move', () => {
      console.log('client moved');
  });

  socket.on('disconnect', () => {
    console.log('client disconnected');
    playerCount--;
  });
});
