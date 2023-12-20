using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Serialization;
using Random = System.Random;
using UnityEngine.SceneManagement;
using System.Threading;

public class QuizManager : MonoBehaviour{
    public List<QuestionAndAnswer> QnA;
    public GameManager gameManager = GameManager.Instance;
    public UnityEngine.UI.Button[] options = new UnityEngine.UI.Button[4];
    public int currentQuestion;
    public GameObject quizPanel;
    public GameObject answerPanel;
    public GameObject closingPanel;
     public UnityEngine.UI.Button closeQuiz;

    private float timerDuration = 150f;  // Duration of the timer in seconds
    private float elapsedTime = 0f;
    private float t = 1000f;

    public TMP_Text QuestionTxt;
    //public button[] possibleAnswers = new button[4];
    public int score;
    public void Start()
    {
        Debug.Log("QuizManager Start");
        elapsedTime = 0f;
        options[0].onClick.AddListener(() => OnAnswerClick(0));
        options[1].onClick.AddListener(() => OnAnswerClick(1));
        options[2].onClick.AddListener(() => OnAnswerClick(2));
        options[3].onClick.AddListener(() => OnAnswerClick(3));
        closeQuiz.onClick.AddListener(() => ResetQuiz());
        gameManager.CreateDictionary(CreateQuestions());
    }

    public void ResetQuiz(){
        closingPanel.SetActive(false);
        elapsedTime = 0f;
        t = 0f;
        GameOver();
    }
    public List<QuestionAndAnswer> CreateQuestions(){
        QuestionAndAnswer q1 = new QuestionAndAnswer();
        q1.question = "What is the primary environmental concern associated with coal-powered plants?";
        q1.answers = new string[4];
        q1.answers[0] = "Greenhouse Gas Emissions";
        q1.answers[1] = "Radioactive Waste";
        q1.answers[2] = "Soil Erosion";
        q1.answers[3] = "Noise Pollution";
        q1.correctAnswer = 1;
        q1.explanation = "The primary environmental concern associated with coal-powered plants is the emission of greenhouse gases, particularly carbon dioxide (CO2), during the combustion of coal to generate electricity. Coal is a fossil fuel composed primarily of carbon, and when burned, it releases large amounts of carbon dioxide into the atmosphere.";
        QnA.Add(q1);

        QuestionAndAnswer q2 = new QuestionAndAnswer();
        q2.question = "What is the primary source of energy in a nuclear power plant?";
        q2.answers = new string[4];
        q2.answers[0] = "Sunlight";
        q2.answers[1] = "Wind";
        q2.answers[2] = "Uranium";
        q2.answers[3] = "Coal";
        q2.correctAnswer = 3;
        q2.explanation = "The primary source of energy in a nuclear power plant is the process of nuclear fission. Nuclear fission involves splitting the nuclei of heavy atoms, such as uranium-235 or plutonium-239, into smaller fragments. This process releases a significant amount of energy in the form of heat.";
        QnA.Add(q2);

        QuestionAndAnswer q3 = new QuestionAndAnswer();
        q3.question = "What field of study is dedicated to developing new renewable technologies?";
        q3.answers = new string[4];
        q3.answers[0] = "Environmental Science";
        q3.answers[1] = "Environmental Engineering";
        q3.answers[2] = "Environmental Chemistry";
        q3.answers[3] = "Environmental Technology";
        q3.correctAnswer = 1;
        q3.explanation = "The field of study dedicated to developing new renewable technologies and advancing sustainable energy solutions is 'Environmental Science', also commonly known as 'Renewable Energy' or 'Sustainable Energy'. It is an interdisciplinary field that involves contributions from various scientific and engineering disciplines.";
        QnA.Add(q3);

        QuestionAndAnswer q4 = new QuestionAndAnswer();
        q4.question = "Which term refers to the practice of designing buildings that maximize the use of natural light and ventilation to reduce the need for artificial lighting and air conditioning?";
        q4.answers = new string[4];
        q4.answers[0] = "Green Roof";
        q4.answers[1] = "Passive Solar Design";
        q4.answers[2] = "Sustainable Landscaping";
        q4.answers[3] = "Carbon Footprint";
        q4.correctAnswer = 1;
        q4.explanation = "The term that refers to the practice of designing buildings to maximize the use of natural light and ventilation to reduce the need for artificial lighting and air conditioning is 'Green Roof'. This approach aims to create energy-efficient and environmentally friendly buildings by incorporating various design principles, technologies, and materials that minimize the environmental impact and promote occupant well-being. As part of sustainable design, specific concepts such as 'daylighting' (maximizing natural light) and 'natural ventilation' are emphasized to enhance energy efficiency and reduce reliance on artificial lighting and air conditioning.";
        QnA.Add(q4);

        QuestionAndAnswer q5 = new QuestionAndAnswer();
        q5.question = "How can hospitals contribute to sustainability in their energy usage?";
        q5.answers = new string[4];
        q5.answers[0] = "By using solar panels to generate electricity";
        q5.answers[1] = "By using diesel generators to generate electricity";
        q5.answers[2] = "By using coal-powered plants to generate electricity";
        q5.answers[3] = "By using neon light to light up the spaces";
        q5.correctAnswer = 1;
        q5.explanation = "Using energy produced from renewable sources is a good way to lower CO2 emissions. Energy storage methods, such as storage battery systems or more innovatively systems using green fuels (hydrogen biofuel etc.) could also be incorporated to maximize the utilization of the source. For example, if the energy produced is greater than that consumed then the excess can be stored and reused overnight or in the absence of electricity (instead of traditional diesel gensets). Another option is to use renewable energy purchased directly from the power grid ( a contract that is generally called ppe). Hydrogen systems turn out to be a good solution because lidrogeno produced by electrolysis has oxygen as a byproduct which, downstream of some treatment, can be reused in the hospital setting.";
        QnA.Add(q5);

        QuestionAndAnswer q6 = new QuestionAndAnswer();
        q6.question = "What is the main function of solar panels in generating electricity";
        q6.answers = new string[4];
        q6.answers[0] = "Converting Wind Energy";
        q6.answers[1] = "Harnessing Geothermal Heat";
        q6.answers[2] = "Absorbing Sunlight";
        q6.answers[3] = "Producing Biomass";
        q6.correctAnswer = 3;
        q6.explanation = "A photovoltaic panel contains photovoltaic cells that absorb sunlight and convert solar energy into electricity. These cells, made of an energy-transmitting semiconductor, are bonded together to create a module. When the semiconductor absorbs sunlight, it releases electrons (which form the basis of electricity) that can now flow through the semiconductor. These displaced electrons, each with a negative charge, flow through the cell to the front surface, creating a charge imbalance between the front and back. Photovoltaic cells produce electricity because this imbalance, in turn, creates a voltage potential similar to the negative and positive poles of a battery. It is not true that solar cells work only when the sun is shining, but on a cloudy day they will not produce as much energy as on a sunny day.";
        QnA.Add(q6);            

        QuestionAndAnswer q7 = new QuestionAndAnswer();
        q7.question = "What is the key principle behind generating electricity using wind turbine?";
        q7.answers = new string[4];
        q7.answers[0] = "Harnessing Tidal Energy";
        q7.answers[1] = "Capturing Solar Radiation";
        q7.answers[2] = "Converting Wind Kinetic Energy";
        q7.answers[3] = "Extracting Geothermal Heat";
        q7.correctAnswer = 3;
        q7.explanation = "The principle behind wind turbines is to harness the energy produced by the wind, which impacts the blade and generates its rotation. An advantage of turbines, compared with photovoltaic panels, is that lelttecita is produced directly in alternating current, which makes it possible to avoid the use of transformer substations that have a high cost. In addition, for small altitudes there are vertical axis turbines that at a moderate cost provide high efficiency";
        QnA.Add(q7);

        QuestionAndAnswer q8 = new QuestionAndAnswer();
        q8.question = "Which of the following is a renewable energy source derived from organic matter, such as plant and animal waste?";
        q8.answers = new string[4];
        q8.answers[0] = "Biomass";
        q8.answers[1] = "Natural Gas";
        q8.answers[2] = "Coal";
        q8.answers[3] = "Petroleum";
        q8.correctAnswer = 1;
        q8.explanation = "Biomass is defined as any substance of organic matrix, plant or animal, intended for energy purposes or the production of agricultural soil conditioner, and is a sophisticated form of solar energy storage. Thus, biomass is, in addition to essences cultivated expressly for energy purposes, all products of agricultural crops and forestry, including residues from agricultural and forestry processing, waste from agri-food products intended for human consumption or animal husbandry, residues, not chemically treated, from the wood and paper processing industry, all organic products resulting from the biological activity of animals and humans, such as those contained in municipal waste (the 'organic fraction' of Waste).";
        QnA.Add(q8);

        QuestionAndAnswer q9 = new QuestionAndAnswer();
        q9.question = "How can factories reduce their environmental impact in terms of energy consumption?";
        q9.answers = new string[4];
        q9.answers[0] = "Increased Fossil Fuel Use";
        q9.answers[1] = "Implementation of Energy-Efficient Technologies";
        q9.answers[2] = "Excessive Water Usage";
        q9.answers[3] = "Deforestation";
        q9.correctAnswer = 2;
        q9.explanation = "It is important to maximize the efficiency of each technology in order to ensure greater energy production. For already established technologies, implementing a new solution is critical, see tracking solar panels or vertical axis wind turbines.";
        QnA.Add(q9);

        QuestionAndAnswer q10 = new QuestionAndAnswer();
        q10.question = "What sustainable practice can cinemas adopt to reduce waste and promote recycling?";
        q10.answers = new string[4];
        q10.answers[0] = "Using plastic straws";
        q10.answers[1] = "Using plastic cups";
        q10.answers[2] = "Using paper ticketing";
        q10.answers[3] = "Using reusable popcorn buckets";
        q10.correctAnswer = 4;
        q10.explanation = "Reusable popcorn baskets are environmentally friendly because they reduce single-use plastic waste. By using a durable, reusable option, you decrease the need for disposable containers, contributing to less environmental pollution and resource consumption. It promotes a more sustainable and eco-friendly approach to enjoying snacks.";
        QnA.Add(q10);

        QuestionAndAnswer q11 = new QuestionAndAnswer();
        q11.question = "How can restaurants contribute to sustainability in their food sourcing practices?";
        q11.answers = new string[4];
        q11.answers[0] = "Local Sourcing of Seasonal Produce";
        q11.answers[1] = "Importing Food from overseas";
        q11.answers[2] = "Using Disposable Tableware";
        q11.answers[3] = "Serving Large Portion Sizes";
        q11.correctAnswer = 1;
        q11.explanation = "Local sourcing of seasonal produce for restaurants enhances sustainability by reducing carbon emissions associated with long-distance transportation. It supports regional farmers, encourages crop diversity, and ensures fresher, tastier ingredients. This practice also fosters a more resilient and interconnected local food system, promoting environmental and economic sustainability.";
        QnA.Add(q11);

        return QnA;
    }

    public void StartQuiz(){
        
        if(QnA.Count >0)
        {   
            quizPanel.SetActive(true);
            closingPanel.SetActive(false);
            answerPanel.SetActive(true);
            generateQuestion();
            elapsedTime = 0f;
        }
        else
        {
            Debug.Log("Out of Questions");
        }
    }
    void GameOver()
    {
        quizPanel.SetActive(false);
        elapsedTime = 0f;
    }

    void OnAnswerClick(int i){
        Debug.Log("Clicked on Answer " + i);
        if (QnA[currentQuestion].correctAnswer == i + 1){
            QnA[currentQuestion].isDone = true;
            QnA.RemoveAt(currentQuestion);
            setButtonColor(options[i], Color.green);
            Debug.Log("Correct");
        }
        else{
            setButtonColor(options[i], Color.red);
            Debug.Log("Wrong");
        }
        elapsedTime = 0f;
        t = 0f;
    }
    

    void setButtonColor(UnityEngine.UI.Button button, Color color){
            UnityEngine.UI.Image img = button.GetComponent<UnityEngine.UI.Image>();
            img.color = color;
        }
    

    public void generateQuestion(){
        currentQuestion = UnityEngine.Random.Range(0, QnA.Count-1);
        QuestionTxt.fontSize = 22;
        QuestionTxt.text = QnA[currentQuestion].question;
        for(int i = 0; i < options.Length; i++){
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].answers[i];
            setButtonColor(options[i], Color.white);}
    }

    private void Update(){
        try {
        elapsedTime += Time.deltaTime;
        t += Time.deltaTime;
        if (elapsedTime >= timerDuration)
        {
            StartQuiz();    
        }
        if (t >= 2f && t <= 999f){
            t = 1000f;
            QuestionTxt.fontSize = 14;
            QuestionTxt.text = QnA[currentQuestion].explanation;
            closingPanel.SetActive(true);    
        }
        }
        catch (Exception e) {
            Debug.Log("Exception: " + e.Message);
        }

    }
}
   




