using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TigerMoon;

namespace TigerMoon.Behaviours
{
    /// <summary>
    /// Temporarily flashes white when damage is taken.
    /// </summary>
    public class WhiteFlashBehaviour : MonoBehaviour
    {
        private Material m_whiteFlash, m_originalMaterial;
        private SpriteRenderer m_spriteRenderer;

        private void Awake()
        {
            m_whiteFlash = new Material(Shader.Find("GUI/Text Shader")); ;
            if (m_whiteFlash == null)
            {
                Debug.LogError("WhiteFlash material not found.");
                gameObject.SetActive(false);
            }

            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_originalMaterial = m_spriteRenderer.material;
        }

        public void Flash()
        {
            m_spriteRenderer.material = m_whiteFlash;
            this.AfterTime(0.1f, () => m_spriteRenderer.material = m_originalMaterial);
        }
    }
}