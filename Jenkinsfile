pipeline {        
    agent any
    stages {
        stage('Login') {
            steps {
                sshagent(credentials : ['97c0900f-d6d9-4eeb-abdb-83efb517dc89'] ) {
                    sh 'ssh root@93.115.16.209'
                    sh 'cd ./StimulationApp'
                }
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
