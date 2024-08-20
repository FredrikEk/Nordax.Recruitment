import React, { useState } from "react";
import { Input } from 'reactstrap';
import { WebApiClient } from "../../common/webApiClient";

type Modaltypes = {
  visible: boolean,
  setVisible: (visible: boolean) => void,
  loanCard: {
    name: string,
    email: string,
    amount: number,
  }
}
export default function Modal({ visible, setVisible, loanCard }: Modaltypes) {
  const [file, setFile] = useState<File | null>(null);
  const apiClient = WebApiClient();
  const [submitError, setSubmitError] = useState<null | string>(null);

  const handleUpload = () => {
    const formData = new FormData();
    if (file) {
      formData.append('file', file);
    }
    apiClient.post<{ fileId: string }>('api/loan-application/attachment', formData).then((res) => {

      if (res.fileId) {
        alert('You file has been submitted succesfully!')

      }
    }).catch(e => {
      setSubmitError(e.status + " " + e.statusText);
      e.json().then((json: any) => {
        setSubmitError(e.status + " " + e.statusText + ": " + json);
      });
    })

  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      setFile(e.target.files[0]);
    }
  };

  return (
    <>
      {visible ? (
        <div style={{ position: 'fixed', top: 0, left: 0, width: '100%', height: '100%', background: 'rgba(0, 0, 0, 0.8)', display: 'flex', alignItems: 'center', justifyContent: "center", zIndex: '1000' }}>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '10px', justifyContent: 'center', color: 'black', backgroundColor: 'white', padding: '20px', width: '300px', textAlign: 'start' }} >
            <div style={{ display: "flex", flexDirection: 'row-reverse' }}>
              <button onClick={() => setVisible(false)}> X</button>
            </div>
            <h3>Loan Application</h3>
            <p>Name - {loanCard.name}</p>
            <p>Email - {loanCard.email}</p>
            <p>Amount - {loanCard.amount} Kr</p>
            <p>Do you want to add a file to your application?</p>
            <Input id="file" type="file" onChange={handleFileChange} />
            {file && <button onClick={handleUpload}>Upload a file</button>}
            {submitError ? <div>
              <p>Something went wrong with your submission.</p>
              <p style={{ color: "red" }}>{submitError}</p>
            </div> : null}
          </div>
        </div>
      ) : (
        null
      )}
    </>
  );
}