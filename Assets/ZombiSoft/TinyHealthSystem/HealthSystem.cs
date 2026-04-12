using SupanthaPaul;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Instance;

    [Header("Health UI")]
    public Image currentHealthBar;
    public Image currentHealthGlobe;
    public Text healthText;

    [Header("Health Values")]
    public float hitPoint = 100f;
    public float maxHitPoint = 100f;

    [Header("Mana UI")]
    public Image currentManaBar;
    public Image currentManaGlobe;
    public Text manaText;

    [Header("Mana Values")]
    public float manaPoint = 100f;
    public float maxManaPoint = 100f;

    [Header("Regen")]
    public bool Regenerate = true;
    public float regen = 0.1f;
    private float timeleft = 0.0f;
    public float regenUpdateInterval = 1f;

    public bool GodMode;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGraphics();
        timeleft = regenUpdateInterval;
    }

    void Update()
    {
        if (Regenerate)
            Regen();
    }

    private void Regen()
    {
        timeleft -= Time.deltaTime;

        if (timeleft <= 0.0f)
        {
            if (GodMode)
            {
                HealDamage(maxHitPoint);
                RestoreMana(maxManaPoint);
            }
            else
            {
                HealDamage(regen);
                RestoreMana(regen);
            }

            UpdateGraphics();
            timeleft = regenUpdateInterval;
        }
    }

    // =========================
    // HEALTH
    // =========================

    private void UpdateHealthBar()
    {
        if (currentHealthBar == null) return;

        float ratio = hitPoint / maxHitPoint;

        currentHealthBar.rectTransform.localPosition =
            new Vector3(
                currentHealthBar.rectTransform.rect.width * ratio - currentHealthBar.rectTransform.rect.width,
                0,
                0
            );
    }

    private void UpdateHealthGlobe()
    {
        if (currentHealthGlobe == null) return;

        float ratio = hitPoint / maxHitPoint;

        currentHealthGlobe.rectTransform.localPosition =
            new Vector3(
                0,
                currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height,
                0
            );
    }

    public void TakeDamage(float damage)
    {
        hitPoint -= damage;

        if (hitPoint < 0)
            hitPoint = 0;

        UpdateGraphics();
        StartCoroutine(PlayerHurts());
    }

    public void HealDamage(float heal)
    {
        hitPoint += heal;

        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;

        UpdateGraphics();
    }

    public void SetMaxHealth(float max)
    {
        maxHitPoint += (int)(maxHitPoint * max / 100);
        UpdateGraphics();
    }

    // =========================
    // MANA
    // =========================

    private void UpdateManaBar()
    {
        if (currentManaBar == null) return;

        float ratio = manaPoint / maxManaPoint;

        currentManaBar.rectTransform.localPosition =
            new Vector3(
                currentManaBar.rectTransform.rect.width * ratio - currentManaBar.rectTransform.rect.width,
                0,
                0
            );
    }

    private void UpdateManaGlobe()
    {
        if (currentManaGlobe == null) return;

        float ratio = manaPoint / maxManaPoint;

        currentManaGlobe.rectTransform.localPosition =
            new Vector3(
                0,
                currentManaGlobe.rectTransform.rect.height * ratio - currentManaGlobe.rectTransform.rect.height,
                0
            );
    }

    public void UseMana(float mana)
    {
        manaPoint -= mana;

        if (manaPoint < 0)
            manaPoint = 0;

        UpdateGraphics();
    }

    public void RestoreMana(float mana)
    {
        manaPoint += mana;

        if (manaPoint > maxManaPoint)
            manaPoint = maxManaPoint;

        UpdateGraphics();
    }

    public void SetMaxMana(float max)
    {
        maxManaPoint += (int)(maxManaPoint * max / 100);
        UpdateGraphics();
    }

    // =========================
    // UI UPDATE
    // =========================

    private void UpdateGraphics()
    {
        UpdateHealthBar();
        UpdateHealthGlobe();
        UpdateManaBar();
        UpdateManaGlobe();

        if (healthText != null)
            healthText.text = hitPoint.ToString("0") + "/" + maxHitPoint.ToString("0");

        if (manaText != null)
            manaText.text = manaPoint.ToString("0") + "/" + maxManaPoint.ToString("0");
    }

    // =========================
    // DAMAGE / DEATH
    // =========================

    IEnumerator PlayerHurts()
    {
        if (hitPoint <= 0)
        {
            yield return StartCoroutine(PlayerDied());
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator PlayerDied()
    {
        Debug.Log("💀 MORISTE");

        // desactivar control
        var controller = GetComponent<PlayerController>();
        if (controller != null)
            controller.enabled = false;

        yield return new WaitForSeconds(2f);

        // CAMBIAR A MENÚ
        SceneManager.LoadScene("MenuPrincipal");
    }
}