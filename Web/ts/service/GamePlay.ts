export class GamePlay {
    constructor() {
        let row = document.querySelector('.mastermind-row');
        let board = document.querySelector('.mastermind-board');
        for (var i = 0; i < 10; i++) {
            board.appendChild(row.cloneNode(true));
        }
    }
}