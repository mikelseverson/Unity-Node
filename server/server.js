var io = require('socket.io')(process.env.PORT || 3000);

console.log('server started');

io.on('connection', (socket) => {
  console.log('client spawned');
  socket.broadcast.emit('spawn');

  socket.on('move', () => {
      console.log('client moved');
  });
});
