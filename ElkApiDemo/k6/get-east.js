import http from 'k6/http';
import { check, sleep } from 'k6';
import { group } from 'k6';

export let options = {
    //thresholds: {
    //    "http_req_duration": ["p(99)<" + __ENV.MAX_REQ_DURATION]
    //},
    insecureSkipTLSVerify: true,
    stages: [
        { duration: '10s', target: 5 },
        { duration: '20s', target: 10 },
        { duration: '30s', target: 20 }
    ]
};

const SLEEP_DURATION = 1;
const apiHost = "http://localhost:5001/weatherforecast/get-east";


export default function () {

    group('Get-east ytelsestester', _ => {

        group('Get east', _ => {
            let response = http.get(apiHost);

            check(response, {
                'status is 200': r => r.status === 200
            });

            sleep(SLEEP_DURATION);
        });
    });
}