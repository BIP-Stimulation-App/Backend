pipeline {        
    agent any
    stages {
        stage('Login') {
            steps {
                    sh 'ssh root@93.115.16.209 '
                    sh 'cd ./StimulationApp'
            }
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
