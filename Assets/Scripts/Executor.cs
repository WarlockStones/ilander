using System;
using UnityEngine;

/* This is (almost) the ONLY gameObject that inherits from MonoBehaviour.
   It manages all other of those functions through interfaces, to manage execution order.
   Some objects that only control their own data like, Animations, Effects, and Complex Behaviour.
   Things that are not dependent to run per tick on outside data can also be normal MonoBehaviours. */
public class Executor : MonoBehaviour {
    private Player player;

    private void Start() {
        player = new Player();
        player.Initialize();
    }

    private void FixedUpdate() {
        player.FixedTick();
    }

    private void Update() {
        player.Tick();

    }

    private void OnDestroy() {
        player.Terminate();
    }

}

/* Executor Interfaces */
public interface ITick {
    public void Tick();
}

public interface IInitialize {
    public void Initialize();
}

public interface ITerminate {
    public void Terminate();
}

public interface IFixedTick {
    public void FixedTick();
}
