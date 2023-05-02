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
                                sh 'docker rmi temptica/stimulation-app-api'
                        }
                }
                stage('Deploy') {
                        steps {
                            dir('StimulationApp'){
                                sh 'docker-compose up -d'
                            }
                        }
                }
        
        }
}
