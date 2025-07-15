using System;
using UnityEngine;

public class State{
    // Nome que pode ser atribuido apenas uma vez durante a sua criação
    public readonly String name;

    // construtor do estado com a unica variavel nome; só pode criara esse estado se ele tiver um nome
    public State(string name){this.name=name;}

    // Metodos que podem ser sobrescritos
    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void Update(){}
}