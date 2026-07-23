// Replace a standard text field with a custom control that lets the user view and edit a project name using an Edit / Save button.

import { IInputs, IOutputs } from "./generated/ManifestTypes";

export class EditablePCF implements ComponentFramework.StandardControl<IInputs, IOutputs> {
    private context: ComponentFramework.Context<IInputs>;
    private container: HTMLDivElement;
    private notifyOutputChanged: () => void;
    private isEditMode: boolean;
    private name: string | null;

    constructor() {
        // Empty
    }

    public init(context: ComponentFramework.Context<IInputs>, notifyOutputChanged: () => void, state: ComponentFramework.Dictionary, container: HTMLDivElement) {
        this.context = context;
        this.notifyOutputChanged = notifyOutputChanged;
        this.container = container;
        this.isEditMode = false;

        const message = document.createElement("span");
        message.innerText = `Project name: ${this.isEditMode ? "" : context.parameters.Name.raw}`;

        const text = document.createElement("input");
        text.type = "text";
        text.style.display = this.isEditMode ? "block" : "none";

        if (context.parameters.Name.raw) {
            text.value = context.parameters.Name.raw;
            
            const messageContainer = document.createElement("div");
            messageContainer.appendChild(message);
            messageContainer.appendChild(text);

            const button = document.createElement("button");
            button.textContent = this.isEditMode ? "Save" : "Edit";
            button.addEventListener("click", () => { this.buttonClick(); });

            this.container.appendChild(messageContainer);
            this.container.appendChild(button);
        }
    }

    public buttonClick() {
        const text = this.container.querySelector("input")!;
        const message = this.container.querySelector("span")!;
        const button = this.container.querySelector("button")!;

        if (!this.isEditMode) {
            text.value = this.name ?? "";
        }
        else if (text.value != this.name) {
            this.name = text.value;
            this.notifyOutputChanged();
        }

        this.isEditMode = !this.isEditMode;

        message.innerText = `Project name: ${this.isEditMode ? "" : this.name}`;
        text.style.display = this.isEditMode ? "inline" : "none";
        text.value = this.name ?? "";
        button.textContent = this.isEditMode ? "Save" : "Edit";
    }
  
    public updateView(context: ComponentFramework.Context<IInputs>): void {
        this.name = context.parameters.Name.raw;
        const message = this.container.querySelector("span")!;
        message.innerText = `Project name: ${this.name}`;
    }

    public getOutputs(): IOutputs {
        return {
            Name: this.name ?? undefined
        };
    }

    public destroy(): void {
        this.container.querySelector("button")!.removeEventListener("click", this.buttonClick);
    }
}
