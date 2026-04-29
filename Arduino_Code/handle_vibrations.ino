const int leftMotor = 3; // Purple cable - LEFT
const int rightMotor = 11; // Brown cable - BACK
const int upMotor = 6; // Yellow cable - UP
const int downMotor = 5; // Orange cable - BACK
const int forwardMotor = 10; // Blue cable - RIGHT  
const int backMotor = 9; // Green cable - FORWARD

String command = "";
char incomingChar;

void setup() {
  Serial2.begin(115200);   

  // Set motor PINs
  pinMode(leftMotor, OUTPUT);
  pinMode(rightMotor, OUTPUT);
  pinMode(upMotor, OUTPUT);
  pinMode(downMotor, OUTPUT);
  pinMode(forwardMotor, OUTPUT);
  pinMode(backMotor, OUTPUT);

  analogWrite(leftMotor, 0);
  analogWrite(rightMotor, 0);
  analogWrite(upMotor, 0);
  analogWrite(downMotor, 0);
  analogWrite(forwardMotor, 0);
  analogWrite(backMotor, 0);

  Serial2.println("Motor control started. Example: LEFT:120");
}

void loop() {
  while (Serial2.available()) {
    incomingChar = Serial2.read();

    if (incomingChar == '\n') {
      command.trim();
      handleMotorCommand(command);
      command = ""; // Reset command
    } else {
      command += incomingChar;
    }
  }
}

void handleMotorCommand(String cmd) {
  int sepIndex = cmd.indexOf(':');
  if (sepIndex == -1) {
    return;
  }

  String motorName = cmd.substring(0, sepIndex);
  int intensity = cmd.substring(sepIndex + 1).toInt();
  intensity = constrain(intensity, 0, 255);

  if (motorName == "LEFT") analogWrite(leftMotor, intensity);
  else if (motorName == "RIGHT") analogWrite(rightMotor, intensity);
  else if (motorName == "UP") analogWrite(upMotor, intensity);
  else if (motorName == "DOWN") analogWrite(downMotor, intensity);
  else if (motorName == "FORWARD") analogWrite(forwardMotor, intensity);
  else if (motorName == "BACK") analogWrite(backMotor, intensity);
}
