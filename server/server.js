"use strict";

const PORT = process.env.PORT || 3000
const io = require('socket.io')(PORT);
const shortid = require('shortid');

console.log('server started');

var playerCount = 0;

io.on('connection', (socket) => {
  let thisClientId = shortid.generate();

  console.log('client connected, broadcasting spawn, id: ', thisClientId);

  socket.broadcast.emit('spawn', {id: thisClientId});
  playerCount++;

  for(let i=0; i < playerCount; i++) {
    console.log('sending spawn to new player')
    socket.emit('spawn');
  };

  socket.on('move', (data) => {
      console.log('client moved', JSON.stringify(data));
      socket.broadcast.emit('move', data);
  });

  socket.on('disconnect', () => {
    console.log('client disconnected');
    playerCount--;
  });
});
