using UnityEngine;

public class PickUpDiamond : MonoBehaviour
{
    // Tag of the player GameObject
    private const string PLAYER_TAG = "Player";

    /// <summary>
    /// Called when a collider enters this trigger collider.
    /// </summary>
    /// <param name="collision">The collider that entered this trigger collider.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the entering collider has the "Player" tag
        if (collision.CompareTag(PLAYER_TAG))
        {
            // Add diamonds to the player's inventory
            Inventory.Instance.AddDiamonds();

            // Increment the count of diamonds picked up in the current scene
            CurrentSceneManager.Instance.DiamondsPickedUpInThisSceneCount++;

            // Deactivate the diamond GameObject to hide it in the scene
            gameObject.SetActive(false);
        }
    }
}
