# Disownership-paradigm

##IMPORTANT: DO NOT COMMIT TO THE MAIN BRANCH WITHOUT PREVIOUS AUTHORIZATION.

### Unity project for the so-called real hand illusion, includes setup GUI, delays webcam feed and uses VAS questionnaires on a VR headset. It should be modified in order to implement similar setups.

The Unity project includes the following scenes in English. 

- Intro: includes GUI for general settings and for participant's information (webcam input, serial port, condition duration, and other selections). A future update should allow users to setup number and name of conditions.

- Instructions: includes the general instructions for the experiment and a short test of the use of the VAS. The instructions should be input directly on the scene by the experimenter.

- StimulationTest: a short trial of how the stimulation will look (without delay) for participants to prepare.

-Inter: the instructions for the specific condition to come (i.e. move your hand from left to right and right to left at a rate of approximately 1Hz). The instructions should be input directly on the scene by the experimenter.

-Stimulation: the actual stimulation, it reads from the ‘Intro’ scene configurations such as delay, whether or not there is white noise on the headphones, duration of the condition, duration before the threat cue, whether to use or not the threat cue and additional settings.

-VAS: the questionnaire that will be presented in the VR headset for particpants. To change the questionnaire edit the questions.csv file located in /Disownership paradigm Unity/Lists/ for Unity Editor mode or in /Lists (you have to create the directory yourself) for standalone mode. The data is logged under /Disownership paradigm Unity/Logs/ for Unity Editor mode or in /Logs (you have to create the directory yourself) for standalone mode.

-Goodbye: simple UI thanking subjects for their participation. 

Technical notes: the serial messages sent out of the serial port depend on the condition and range from 1-4. A future update should allow users to encode this from the GUI.


