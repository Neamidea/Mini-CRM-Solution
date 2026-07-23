import { IInputs, IOutputs } from "./generated/ManifestTypes";

export class FirstControl implements ComponentFramework.StandardControl<IInputs, IOutputs> {

    private label: HTMLInputElement;
    private _container: HTMLDivElement;

    constructor() {

    }

    public init(
        context: ComponentFramework.Context<IInputs>,
        notifyOutputChanged: () => void,
        state: ComponentFramework.Dictionary,
        container: HTMLDivElement
    ): void {
        this.label = document.createElement("input");
        this.label.setAttribute("type", "label");
        this.label.value = "My First PCF";
        this._container = document.createElement("div");
        this._container.appendChild(this.label);
        container.appendChild(this._container);
    }

    public updateView(context: ComponentFramework.Context<IInputs>): void {

    }

    public getOutputs(): IOutputs {
        return {};
    }

    public destroy(): void {
        
    }
}
