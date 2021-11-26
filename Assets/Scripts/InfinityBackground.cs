using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityBackground : MonoBehaviour
{
    [SerializeField] bool horizontalInfinity;
    [SerializeField] int numberBackgroundX;
    [SerializeField] int xOffset;
    [SerializeField] bool verticalInfinity;
    [SerializeField] int numberBackgroundY;
    [SerializeField] int yOffset;
    private Transform cam;
    private Vector3 backgroundPrevPos;
    private SpriteRenderer sprite;
    private float m_nWidthSprite;
    private float m_nHeightSprite;

    void Awake() {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        backgroundPrevPos = transform.position;
        m_nWidthSprite = sprite.bounds.size.x/ numberBackgroundX;
        m_nHeightSprite = sprite.bounds.size.y/ numberBackgroundY;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translate = new Vector3(0, 0, 0);

        if (horizontalInfinity) {
            if (backgroundPrevPos.x - cam.position.x >= xOffset) {
                translate.x = -m_nWidthSprite;
            }
            else if (cam.position.x - backgroundPrevPos.x >= xOffset){
                translate.x = m_nWidthSprite;
            }
        }

        if (verticalInfinity) {
            if (backgroundPrevPos.y - cam.position.y >= yOffset) {
                translate.y = -m_nHeightSprite;
            }
            else if (cam.position.y - backgroundPrevPos.y >= yOffset){
                translate.y = m_nHeightSprite;
            }
        }
        transform.Translate(translate);

        backgroundPrevPos = transform.position;
    }
}
