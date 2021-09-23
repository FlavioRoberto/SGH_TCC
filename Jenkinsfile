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

        stage('Unit Test') {
          steps {
            echo 'Starting Unit Test'
            sh 'dotnet test Api/SGH/SGH.TestesUnitarios'
            echo 'Finished Unit Test'
          }
        }

        stage('Integration Test') {
          steps {
            echo 'Stating Integration tests'
            sh 'dotnet test Api/SGH/SGH.TestesDeIntegracao'
            echo 'Finished integration tests'
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