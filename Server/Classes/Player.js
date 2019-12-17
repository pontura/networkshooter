var shortID = require('shortid');

console.log('caca');

module.exports = class Player{
    constructor()  {
        this.username = '';
        this.id = shortID.generate();
    }
}