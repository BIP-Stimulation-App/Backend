pipeline {        
    agent any
    stages {
        stage('Login') {
            steps {
                sh 'ssh root@93.115.16.209'                
            }
        }
         stage('Build') {
                        steps {
                                sh 'sudo docker rmi temptica/stimulation-app-api'
                        }
                }
                stage('Deploy') {
                        steps {
                            dir('StimulationApp'){
                                sh 'sudo docker compose build'
                            }
                        }
                }
        
        }
}
