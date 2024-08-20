import React, { useState } from "react";
import { Button } from '../common/button/Button';
import { WebApiClient } from "../../common/webApiClient";
import { Container, Input } from 'reactstrap';
import { NewLoanApplicationRequest } from "../../models/newLoanApplicationRequest";


export const LoanApplicationForm = () => {
  const [formData, setFormData] = useState({ name: "", email: "", amount: 0, file: null } as NewLoanApplicationRequest);
  const [submitError, setSubmitError] = useState<null | string>(null);
  const [terms, setTerms] = useState<boolean>(false)
  const apiClient = WebApiClient();

  const handleTermsChange = () => {
    setTerms(!terms)
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const onSubmit = () => {
    apiClient.post<{ loanApplicationId: string }>('api/loan-application', formData)
      .then((res) => {
        if (res.loanApplicationId) {
          alert('You application has been submitted succesfully!')
        }
      }).catch(e => {
        setSubmitError(e.status + " " + e.statusText);
        e.json().then((json: any) => {
          setSubmitError(e.status + " " + e.statusText + ": " + json);
        });
      });

  };

  return (
    <Container>
      <div style={{ display: 'flex', flexDirection: 'column', justifyContent: "center", alignItems: "center", height: '90%', textAlign: "center", gap: '5px', border: '1px solid black' }}>
        <h2>Loan Application Nordax Bank</h2>
        <p>
          Here you can make a loan application, fill in your name, email and amount in Swedish Kroner(SEK) and we will get back to you within 1-3 working days
        </p>
        <Input type={"text"} name={"name"} placeholder={"Name.."} value={formData.name} onChange={handleChange} style={{
          width: "40%",
          margin: "0 auto",
          minWidth: "230px"
        }} />
        <Input type={"email"} name={"email"} placeholder={"Email.."} value={formData.email} onChange={handleChange} style={{
          width: "40%",
          margin: "0 auto",
          minWidth: "230px"
        }} />
        <Input type={"number"} name={"amount"} value={formData.amount} onChange={handleChange} style={{
          width: "40%",
          margin: "0 auto",
          minWidth: "230px"
        }} />
        <div style={{ display: "flex", alignItems: 'center', justifyContent: 'center' }}>
          <p style={{ margin: 0, paddingRight: "8px" }}>You need to accept our Terms and Contions</p>
          <Input type={"checkbox"} checked={terms} onChange={handleTermsChange} />
        </div>
        {submitError ? <div>
          <p>Something went wrong with your submission.</p>
          <p style={{ color: "red" }}>{submitError}</p>
        </div> : null}
        <Button disabled={!terms} onClick={() => onSubmit()} style={{
          width: "20%",
          marginLeft: "5px",
          marginRight: "5px",
          paddingTop: '10px'
        }}>Apply</Button>
      </div>
    </Container >
  )
}

export default LoanApplicationForm