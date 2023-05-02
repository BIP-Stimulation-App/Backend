pipeline {        
    agent any
    stages {
            stage('Build') {
                        steps {
                                sh 'docker rmi temptica/stimulation-app-api'
                        }
                }
                stage('Deploy') {
                        steps {
                                sh 'docker compose build'
                        }
                }
        }
}
