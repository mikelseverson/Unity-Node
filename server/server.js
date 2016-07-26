"use strict";

const PORT = process.env.PORT || 3000
const io = require('socket.io')(PORT);
const shortid = require('shortid');

console.log('server started');

var playerCount = 0;
var players = [];

io.on('connection', (socket) => {
  let thisPlayerId = shortid.generate();
  players.push(thisPlayerId);
  console.log('client connected, broadcasting spawn, id: ', thisPlayerId);

  socket.broadcast.emit('spawn', {id: thisPlayerId});
  playerCount++;

  players.forEach(playerId => {
    if(playerId == thisPlayerId) return;
    socket.emit('spawn', {id: playerId});
  })

  for(let i=0; i < playerCount; i++) {
    console.log('spawning to new player')
    socket.emit('spawn');
  };

  socket.on('move', (data) => {
      data.id = thisPlayerId;
      console.log('client moved', JSON.stringify(data));
      socket.broadcast.emit('move', data);
  });

  socket.on('disconnect', () => {
    console.log('client disconnected');
    playerCount--;
  });
});
