import { MastermindUIElements } from "./MastermindUIElements";
import { CodeColors } from "../enum/CodeColors";
import { CodeFactory } from "../factory/CodeFactory";
import { ResponseColors } from "../enum/ResponseColors";
import { GameState } from "../model/GameState";

export class MastermindUIUtility {

    public static CloneRowNode(numberOfTurns: number) {
        let board = document.querySelector(MastermindUIElements.Board);
        let row = document.querySelector(MastermindUIElements.RowTemplateClass);
        row.className = MastermindUIElements.RowClass.replace('.', '');
        //make an extra one for the answer and an extra one for the template
        let x = numberOfTurns + 1;
        for (var i = 0; i < x; i++) {
            board.appendChild(row.cloneNode(true));
        }
        //find the first one and change its name and hide it        
        let firstChild = document.querySelector(MastermindUIElements.RowClass);
        firstChild.className = `${MastermindUIElements.RowTemplateClass.replace('.', '')} hide`;
        
        //clean up final row
        MastermindUIUtility.CleanFinalNode(board);
    }

    public static MarbleSelect(event: Event, selectedMarble: HTMLElement, selectionRow: HTMLElement) {
        let selected = event.target as HTMLElement;
        let elementPosition = selected.getBoundingClientRect();
        elementPosition = selected.getBoundingClientRect();
        selectedMarble = selected;
        let newPosition = elementPosition.top - 72;
        selectionRow.className = selectionRow.className.replace(' hide', '');
        selectionRow.style.top = `${newPosition}px`;
    }

    public static CleanFinalNode(board: Element) {
        let answerRow = board.querySelector(MastermindUIElements.RowClass) as HTMLElement;
        //let answerRow = rows[rows.length - 1];
        let answerRowGoButton = answerRow.querySelector(MastermindUIElements.RowGoButton);
        let answerRowPegs = answerRow.querySelector(MastermindUIElements.PegsClass);
        answerRowGoButton.remove();
        answerRowPegs.remove();
    }

    public static ApplySelectedColorToMarble(event: Event, selectedMarble: HTMLElement, selectionRow: HTMLElement) {
        let className = (event.target as HTMLElement).className;
        let colorList = className.split(' ');
        let color = colorList[colorList.length - 1];
        selectedMarble.className += ` ${color} hide`;
    }

    public static ShowHiddenRow() {
        let row = document.querySelector(MastermindUIElements.RowTemplateClass);
        if (row.className.indexOf(' ') >= 0) {
            row.className = row.className.split(' ')[0];
        }
    }

    public static SubmitRow(event: Event, game: GameState, selectedMarble: HTMLElement, selectionRow: HTMLElement) {
        let self = this;
        let btn = event.target as HTMLElement;
        let parentRow = btn.parentElement as HTMLElement;
        let activeMarbles = parentRow.querySelectorAll(MastermindUIElements.MarbleTopClass);
        
        let colors: CodeColors[] = [];
        for (var i = 0; i < activeMarbles.length; i++) {
            var marble = activeMarbles[i] as HTMLElement;
            console.log(marble.className);
            colors.push(CodeColors.Green);
        }

        let guess = CodeFactory.CreateFromList(colors);
        let codeResponse = game.Guess(guess);
        
        //show code response in ui
        let pegs = parentRow.querySelectorAll(`${MastermindUIElements.PegsClass} ${MastermindUIElements.PegClass}`);
        let codeResponses: ResponseColors[] = [ codeResponse.One, codeResponse.Two, codeResponse.Three, codeResponse.Four ];
        for(var i = 0; i < pegs.length; i++){
            let response = codeResponses[i];
            let responseClass = '';
            switch(response){
                case ResponseColors.Red:
                    responseClass = ` ${MastermindUIElements.Red}`;
                    break;
                case ResponseColors.White:
                    responseClass = ` ${MastermindUIElements.White}`;
                    break;
                case ResponseColors.None:
                default:
                    break;
            }
            let peg = pegs[i]
            peg.className += responseClass;
        }

        if(game.CodeBroken){
            //game over
            alert('You Won!');
            return;
        }

        MastermindUIUtility.SetEventsForActiveRow(game, selectedMarble, selectionRow);
    }

    

    public static GetRadioVal(formId: string, name: string): string {
        let form = document.getElementById(formId) as HTMLFormElement;
        // get list of radio buttons with specified name
        let radios = form.elements[name];
        for (let i = 0; i < radios.length; i++) {
            if (radios[i].checked) {
                return radios[i].value;
            }
        }
        return '';
    }
    
    
    public static SetupColorSelector(selectedMarble: HTMLElement, selectionRow: HTMLElement) {
        let coloredMarblesSelector = `${MastermindUIElements.SelectionRowClass} ${MastermindUIElements.MarbleTopClass}`;
        let coloredMarbles = document.querySelectorAll(coloredMarblesSelector);
        for (var i = 0; i < coloredMarbles.length; i++) {
            coloredMarbles[i].addEventListener(MastermindUIElements.Click, function (event) {
                MastermindUIUtility.ApplySelectedColorToMarble(event, selectedMarble, selectionRow);
            });
        }
    }

    public static SetEventsForActiveRow(
        game: GameState, 
        selectedMarble: HTMLElement, 
        selectionRow: HTMLElement
    ): void {
        let self = this;
        let activeRow: number = game.Turns.length; 
        let marbleRows = MastermindUIElements.RowClass;
        let activeMarbleRow = document.querySelectorAll(marbleRows)[activeRow];
        let rowGoButton = activeMarbleRow.querySelector(MastermindUIElements.RowGoButton);
        rowGoButton.addEventListener(MastermindUIElements.Click, function(event){
            MastermindUIUtility.SubmitRow(event, game, selectedMarble, selectionRow);
        });
        let activeMarbles = activeMarbleRow.querySelectorAll(MastermindUIElements.MarbleTopClass);
        for (var i = 0; i < activeMarbles.length; i++) {
            activeMarbles[i].addEventListener(MastermindUIElements.Click, function (event) {
                MastermindUIUtility.MarbleSelect(event, selectedMarble, selectionRow);
            });
        }
    }
}