import React, { useEffect, useState } from "react";
import { WebApiClient } from "../../common/webApiClient";
import { Col, Container, Row } from 'reactstrap';
import { Logo } from "../common/logo/Logo";

import Modal from "./loanApplicationModal";


type LoanModel = {
  name: string,
  email: string,
  amount: number,
}

type LoanModelList = {
  id: string
  name: string,
  email: string,
  amount: number,
}

const LoanApplicationList = () => {
  const [loans, setLoans] = useState<LoanModelList[]>([]);
  const [loanCard, setLoanCard] = useState<LoanModel>({
    name: '', email: '', amount: 0
  });
  const [submitError, setSubmitError] = useState<null | string>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [visible, setVisible] = useState<boolean>(false);
  const apiClient = WebApiClient();



  const cardItem = (id: string) => {
    if (id) {
      apiClient.get<LoanModel>(`api/loan-application/${id}`).then((res) => {
        if (!res) {
          setIsLoading(true)
        } else {
          setVisible(true)
          setLoanCard(res)
          setIsLoading(false)
        }
      }).catch(e => {
        setSubmitError(e.status + " " + e.statusText)
        e.json().then((json: any) => {
          setSubmitError(e.status + " " + e.statusText + ": " + json);
        })
      });
    }

  }
  useEffect(() => {
    apiClient.get<LoanModelList[]>('api/loan-application').then((res) => {
      if (!res) {
        setIsLoading(true)
      } else {
        const data = res;
        setLoans(data)
        setIsLoading(false)
      }
    }).catch(e => {
      setSubmitError(e.status + " " + e.statusText);
      e.json().then((json: any) => {
        setSubmitError(e.status + " " + e.statusText + ": " + json);
      })
    });
  }, [])


  return (
    <Container style={{ height: '100vh', overflow: 'hidden' }} >
      <Row className="justify-content-md-center align-items-center" style={{ paddingTop: '20px' }}>
        <Col md="5" className="text-center">
          <Logo />
          {isLoading ? <div>Loading...</div> : (
            <div style={{ padding: '5px', maxHeight: '70vh', overflowY: 'auto' }}>
              <ul style={{ listStyle: 'none', padding: '0', margin: '0' }}>
                {loans.map(loan => (
                  <li
                    style={{
                      height: '100%',
                      borderRadius: '10px',
                      marginBottom: '10px',
                      border: '1px solid black',
                      padding: '10px',
                      cursor: 'pointer'
                    }}
                    key={loan.id}
                    onClick={() => cardItem(loan.id)}
                  >
                    <div style={{ display: 'flex', flexDirection: 'column', gap: '10px', justifyContent: 'center' }}>
                      <p>Name - {loan.name}</p>
                    </div>
                  </li>
                ))}
              </ul>
            </div>
          )}
          {submitError ? (
            <div>
              <p>Something went wrong with your submission.</p>
              <p style={{ color: "red" }}>{submitError}</p>
            </div>) : null}
        </Col>
        <Modal visible={visible} setVisible={setVisible} loanCard={loanCard} />
      </Row>
    </Container>
  )
}

export default LoanApplicationList