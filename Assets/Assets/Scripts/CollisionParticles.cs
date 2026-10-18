using UnityEngine;

public class CollisionParticles : MonoBehaviour
{
    [Header("Efeitos de partículas")]
    [SerializeField] private ParticleSystem rockParticles;
    [SerializeField] private ParticleSystem fruitParticles;
    [SerializeField] private ParticleSystem loveParticles;

    public void PlayParticles(string tag, Vector2 collisionPoint)
    {
        ParticleSystem selectedParticles = null;

        if (tag == "Rock" || tag == "Box")
            selectedParticles = rockParticles;
        else if (tag == "Fruit")
            selectedParticles = fruitParticles;
        else if (tag == "Love")
            selectedParticles = loveParticles;

        if (selectedParticles == null)
            return;

        // Posiciona o efeito no ponto exato da colisão
        selectedParticles.transform.position = collisionPoint;

        // Reinicia e reproduz o efeito
        selectedParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        selectedParticles.Play();
    }
}