using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RawImage vehicleImage;               // Image de rendu de la cam�ra
    public List<CarSO> carList = new List<CarSO>();  // Liste des v�hicules disponibles
    public Transform[] spawnPoints;             // Tableau de points de pose
    private int currentVehicleIndex = 0;        // Index du v�hicule actue

    private CarSO currentCar;                   // R�f�rence � la voiture actuellement s�lectionn�e
    private List<GameObject> instantiatedCars = new List<GameObject>(); // Liste des v�hicules instanci�s

    void Start()
    {
        currentVehicleIndex = 0;
    }
    void Update()
    {
        NextVehicle();
        PreviousVehicle();
    }
    // M�thode pour changer le v�hicule affich�
    public void SwitchVehicle(CarSO car)
    {
        // D�finir la voiture actuelle
        currentCar = car;

        // Mettre � jour l'image avec la RenderTexture de la nouvelle voiture
        vehicleImage.texture = currentCar.image;

        // Activer le GameObject associ� au v�hicule s�lectionn�
        foreach (var carSO in carList)
        {
            if (carSO.gameOject != null)
                carSO.gameOject.SetActive(carSO == currentCar);
        }
    }

    // M�thode pour passer au v�hicule suivant dans la liste
    public void NextVehicle()
    {
        currentVehicleIndex = (currentVehicleIndex + 1) % carList.Count;
        SwitchVehicle(carList[currentVehicleIndex]);
    }

    // M�thode pour passer au v�hicule pr�c�dent dans la liste
    public void PreviousVehicle()
    {
        currentVehicleIndex = (currentVehicleIndex - 1 + carList.Count) % carList.Count;
        SwitchVehicle(carList[currentVehicleIndex]);
    }

    // M�thode pour changer la couleur en fonction de la couleur du bouton s�lectionn�
    public void ChangeColor(Button colorButton)
    {
        if (currentCar != null && currentCar.color != null)
        {
            // R�cup�rer la couleur de l'image du bouton
            Color selectedColor = colorButton.GetComponent<Image>().color;

            // Appliquer la couleur au mat�riau de la voiture actuelle
            currentCar.color.color = selectedColor;
        }
    }

    // M�thode pour s�lectionner et instancier la voiture actuelle sur un point de pose
    public void CarSelected()
    {
        // D�truire tous les v�hicules d�j� instanci�s pour �viter les doublons
        foreach (var car in instantiatedCars)
        {
            if (car != null)
                Destroy(car);
        }
        instantiatedCars.Clear();  // Vider la liste des v�hicules instanci�s

        // Instancier le v�hicule sur chaque point de pose
        if (currentCar != null && currentCar.gameOject != null)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                GameObject newCar = Instantiate(currentCar.gameOject, spawnPoint.position, spawnPoint.rotation);
                newCar.transform.SetParent(spawnPoint, true);
                instantiatedCars.Add(newCar);  // Ajouter � la liste des v�hicules instanci�s
            }
        }
    }
}