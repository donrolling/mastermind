import { MastermindUIElements } from "./MastermindUIElements";
import { CodeColors } from "../enum/CodeColors";
import { CodeFactory } from "../factory/CodeFactory";
import { ResponseColors } from "../enum/ResponseColors";
import { GameState } from "../model/GameState";

export class MastermindUIUtility {
    public static ApplySelectedColorToMarble(event: Event) {
        let className = (event.target as HTMLElement).className;
        let colorList = className.split(' ');
        let color = colorList[colorList.length - 1];
        let selectedMarble = document.querySelector('.marble.empty.selected');
        if(selectedMarble){
            selectedMarble.className = selectedMarble.className.replace(` ${MastermindUIElements.Selected}`, '');
            selectedMarble.className += ` ${color}`;
        }
        let selectionRow = MastermindUIUtility.getSelectionRow();
        selectionRow.className += ' hide';
    }

    public static MarbleSelect(event: Event) {
        let selected = event.target as HTMLElement;
        let elementPosition = selected.getBoundingClientRect();
        elementPosition = selected.getBoundingClientRect();
        MastermindUIUtility.deselectSelectedMarble();
        selected.className += ` ${MastermindUIElements.Selected}`;

        let newPosition = elementPosition.top - 92;
        let selectionRow = MastermindUIUtility.getSelectionRow();
        selectionRow.className = selectionRow.className.replace(' hide', '');
        selectionRow.style.top = `${newPosition}px`;
    }

    private static deselectSelectedMarble() {
        let previouslySelected = MastermindUIUtility.getSelectedMarble();
        if (previouslySelected) {
            previouslySelected.className = previouslySelected.className.replace(` ${MastermindUIElements.Selected}`, '');
        }
    }

    private static getSelectedMarble(): HTMLElement{
        return document.querySelector('.marble-crater div.selected') as HTMLElement;
    }
    
    private static getSelectionRow(): HTMLElement{
        return document.querySelector(MastermindUIElements.SelectionRowClass) as HTMLElement;
    }
    
    public static SetupColorSelector() {
        let coloredMarblesSelector = `${MastermindUIElements.SelectionRowClass} ${MastermindUIElements.MarbleTopClass}`;
        let coloredMarbles = document.querySelectorAll(coloredMarblesSelector);
        for (var i = 0; i < coloredMarbles.length; i++) {
            coloredMarbles[i].addEventListener(MastermindUIElements.Click, function (event) {
                MastermindUIUtility.ApplySelectedColorToMarble(event);
            });
        }
    }

    public static SetEventsForActiveRow(game: GameState): void {
        let self = this;
        let activeRow: number = game.Turns.length + 1;
        let marbleRows = MastermindUIElements.RowClass;
        let activeMarbleRow = document.querySelectorAll(marbleRows)[activeRow];
        let rowGoButtons = document.querySelectorAll(`${MastermindUIElements.RowClass} ${MastermindUIElements.RowGoButton}`);
        for(var i = 0;i < rowGoButtons.length;i++){
            let b = rowGoButtons[i];
            if(i === activeRow){
                b.className = b.className.replace(' hide', '');
            } else {
                b.className += ' hide';
            }
        }
        let rowGoButton = rowGoButtons[activeRow];
        rowGoButton.addEventListener(MastermindUIElements.Click, function(event){
            MastermindUIUtility.SubmitRow(event, game);
        });
        let activeMarbles = activeMarbleRow.querySelectorAll('.marble.empty');
        for (var i = 0; i < activeMarbles.length; i++) {
            activeMarbles[i].addEventListener(MastermindUIElements.Click, function (event) {
                MastermindUIUtility.MarbleSelect(event);
            });
        }
    }

    public static SubmitRow(event: Event, game: GameState) {
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

        MastermindUIUtility.SetEventsForActiveRow(game);
    }
}