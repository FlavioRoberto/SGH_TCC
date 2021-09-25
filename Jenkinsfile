pipeline {
  agent any
  stages {
    stage('Build') {
      parallel {
        stage('Build') {
          steps {
            echo 'Starting Build SGH'
          }
        }

        stage('Test') {
          steps {
            echo 'Starting Unit Test'
            sh 'dotnet test Api/SGH/SGH.TestesUnitarios'
            echo 'Finished Unit Test'
            echo 'Starting integration tests'
            sh 'dotnet test Api/SGH/SGH.TestesDeIntegracao'
            echo 'Finished integration tests'
          }
        }

        stage('Test Log') {
          steps {
            writeFile(file: 'LogTestFile.txt', text: 'This is an automation file')
          }
        }

      }
    }

    stage('Deploy') {
      steps {
        echo 'Starting deploy'
      }
    }

  }
}