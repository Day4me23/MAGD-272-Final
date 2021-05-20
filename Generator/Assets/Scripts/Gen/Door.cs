using UnityEngine;
using System.Collections;
public class Door : MonoBehaviour
{
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool left;
    [SerializeField] bool right;

    [SerializeField]Tile tile;

    private void Start() => tile = GetComponentInParent<Space>().tile;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Tile link = null;
            if (up) link = Generator.instance.tiles[tile.pos + Vector2.up];
            else if (down) link = Generator.instance.tiles[tile.pos + Vector2.down];
            else if (left) link = Generator.instance.tiles[tile.pos + Vector2.left];
            else if (right) link = Generator.instance.tiles[tile.pos + Vector2.right];

            StartCoroutine(Pause(Transition.instance.DoTransition(), collision.transform, link));
        }
    }

    IEnumerator Pause(float time, Transform collision, Tile link)
    {
        yield return new WaitForSeconds(time);

        Builder.instance.ActivateRoom(link.roomNumber);
        collision.transform.position = link.pos;

        StopCoroutine(Pause(0, null, null));
    }
}
