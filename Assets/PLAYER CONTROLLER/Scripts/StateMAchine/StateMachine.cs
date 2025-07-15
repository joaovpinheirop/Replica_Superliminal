using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    // Estado atual
    private State currentState;

    // Nome do estado atual;
    public string currentStateName { get ; private set; }

    // Fazer o upload que esta no estado;
    public void Update() => currentState?.Update();

    // Muda o novo estado.
    public void ChangeState(State newState){
        // verificar se esta em um estado se estiver sair dele
        currentState?.Exit();
        // mudar para o novo estado
        currentState = newState;
        // mudar o nome do estado
        currentStateName = newState.name;
        // entrar no estado
        newState.Enter();
    }
}
