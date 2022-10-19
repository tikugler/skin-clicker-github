using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestSettingsPopUp
{
    // look for inactive objects in canvas
    private GameObject canvasGameObject;


    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        SceneManager.LoadScene("StartMenu");

    }


    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return null;
        canvasGameObject = GameObject.Find("Canvas");
    }


    /// <summary>
    /// Settings Pop-Up should be inactive when the scene is first loaded
    /// </summary>
    [UnityTest]
    public IEnumerator SettingPopOpIsNotActiveWhenTheSceneLoaded()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        yield return null;
        canvasGameObject = GameObject.Find("Canvas");
        GameObject settingsPopUp = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "SettingsPopUp");
        yield return null;
        Assert.AreEqual(false, settingsPopUp.activeSelf);
        settingsPopUp.SetActive(false);
    }


    /// <summary>
    ///After clicking Settings Button, Setting Pop-Up should be active
    /// </summary>
    [UnityTest]
    public IEnumerator SettingButtonOpensPopUpWindow()
    {
        GameObject settingsPopUp = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "SettingsPopUp");
        settingsPopUp.SetActive(false);
        Button settingButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "SettingsButton").
            GetComponent<Button>();
        Assert.AreEqual(false, settingsPopUp.activeSelf);
        settingButton.onClick.Invoke();
        yield return null;
        Assert.AreEqual(true, settingsPopUp.activeSelf);
    }


    /// <summary>
    /// Settings Pop-Up should be closed, when close button is pressed
    /// </summary>
    [UnityTest]
    public IEnumerator CloseButtonOnSettingsPopUpClosesSettingPopUpWindow()
    {
        GameObject settingsPopUp = FindObjectHelper.FindObjectInParent(canvasGameObject, "SettingsPopUp");
        settingsPopUp.SetActive(true);
        Assert.AreEqual(true, settingsPopUp.activeSelf);

        Button closeButton = GameObject.Find("CloseSettingsPopUpButton").GetComponent<Button>();
        closeButton.onClick.Invoke();

        yield return null;
        Assert.AreEqual(false, settingsPopUp.activeSelf);
    }


    /// <summary>
    /// The state (mute/unmute) of Music Audio Player is changed
    /// according to Music Toggle
    /// </summary>
    [UnityTest]
    public IEnumerator MusicCToggleMutesAndUnmutesMusic()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Toggle musicToggle = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "MusicToggle").GetComponent<Toggle>();

        musicToggle.isOn = false;
        yield return null;
        Assert.AreEqual(false, gameManager.MusicAudio.isPlaying);

        musicToggle.isOn = true;
        yield return null;
        Assert.AreEqual(true, gameManager.MusicAudio.isPlaying);
    }



    /// <summary>
    /// The state (mute/unmute) of Effect Audio Player is changed
    /// according to Effect Toggle
    /// </summary>
    [UnityTest]
    public IEnumerator EffectToggleMutesAndUnmutesMusic()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Toggle effectToggle = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "EffectToggle").GetComponent<Toggle>();

        effectToggle.isOn = false;
        yield return null;
        Assert.AreEqual(false, gameManager.EffectAudio.isPlaying);

        effectToggle.isOn = true;
        yield return null;
        Assert.AreEqual(true, gameManager.EffectAudio.isPlaying);
    }


    /// <summary>
    /// The volume of Music Audio Player is changed accord to Music Slider
    /// </summary>
    [UnityTest]
    public IEnumerator MusicSliderUpdatesMusicVolume()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Slider musicSlider = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "MusicSlider").GetComponent<Slider>();

        float sliderNewValue;

        sliderNewValue = 0.25f;
        musicSlider.value = sliderNewValue;
        yield return null;
        Assert.AreEqual(sliderNewValue, gameManager.MusicAudio.volume, 0.001);

        sliderNewValue = 0.99f;
        musicSlider.value = sliderNewValue;
        yield return null;
        Assert.AreEqual(sliderNewValue, gameManager.MusicAudio.volume, 0.001);

        sliderNewValue = 0f;
        musicSlider.value = sliderNewValue;
        yield return null;
        Assert.AreEqual(sliderNewValue, gameManager.MusicAudio.volume, 0.001);


    }


    /// <summary>
    /// The volume of Music Audio Player is changed accord to Effect Slider
    /// </summary>
    [UnityTest]
    public IEnumerator EffectSliderUpdatesMusicVolume()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Slider effectSlider = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "EffectSlider").GetComponent<Slider>();

        float sliderNewValue;

        sliderNewValue = 0.25f;
        effectSlider.value = sliderNewValue;
        yield return null;
        Assert.AreEqual(sliderNewValue, gameManager.EffectAudio.volume, 0.001);

        sliderNewValue = 0.99f;
        effectSlider.value = sliderNewValue;
        yield return null;
        Assert.AreEqual(sliderNewValue, gameManager.EffectAudio.volume, 0.001);

        sliderNewValue = 0f;
        effectSlider.value = sliderNewValue;
        yield return null;
        Assert.AreEqual(sliderNewValue, gameManager.EffectAudio.volume, 0.001);
    }


}


