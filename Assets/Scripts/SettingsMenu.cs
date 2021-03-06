﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    public Dropdown resolutionsDropDown, qualitiesDropDown;
    public Slider volumeSlider;
    public Toggle windowMode;
    int resolutionIndexValue, qualityIndexValue;
    float volumeValue;
    int windowModeValue;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropDown.AddOptions(options);

        //Verifica se exitem os playerPrefs, caso não, usa o padrão
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolutionIndexValue = PlayerPrefs.GetInt("Resolution");
            resolutionsDropDown.value = resolutionIndexValue;
        }
        else
        {
            
            resolutionsDropDown.value = currentResolutionIndex;
            resolutionsDropDown.RefreshShownValue();
            
        }

        //Verifica se exitem os playerPrefs, caso não, usa o padrão
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeValue = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = volumeValue;
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 1);
            volumeSlider.value = 1;
        }

        //Verifica se exitem os playerPrefs, caso não, usa o padrão
        if (PlayerPrefs.HasKey("Quality"))
        {
            qualityIndexValue = PlayerPrefs.GetInt("Quality");
            qualitiesDropDown.value = qualityIndexValue;
        }
        else
        {
            SetQuality(2);
            qualitiesDropDown.value = 2;
        }

        //Verifica se exitem os playerPrefs, caso não, usa o padrão
        if (PlayerPrefs.HasKey("WindowMode"))
        {
            windowModeValue = PlayerPrefs.GetInt("WindowMode");
            if(windowModeValue == 1)
            {
                windowMode.isOn = true;
            }
            else
            {
                windowMode.isOn = false;
            }
        }
        else
        {
            SetWindowMode(false);
            windowMode.isOn = false;
        }

    }

    //Setar o volume
    public void SetVolume(float volume)
    {
        volumeValue = volume;
        AudioListener.volume = volume;
    }

    //Setar a resolução do jogo
    public void SetResolution(int resolutionIndex)
    {
        resolutionIndexValue = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Setar a qualidade do jogo
    public void SetQuality(int qualityIndex)
    {
        qualityIndexValue = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Mudar o modo Janela ou não
    public void SetWindowMode(bool isWindowMode)
    {
        if (isWindowMode)
        {
            windowModeValue = 1;
        }
        else
        {
            windowModeValue = 0;
        }
        Screen.fullScreen = !isWindowMode;
    }

    //Salvar as preferências do player
    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", volumeValue);
        PlayerPrefs.SetInt("Quality", qualityIndexValue);
        PlayerPrefs.SetInt("WindowMode", windowModeValue);
        PlayerPrefs.SetInt("Resolution", resolutionIndexValue);
    }
}
